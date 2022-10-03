import * as Yup from 'yup';
import { Link } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import useLogin from '../../hooks/authService/useLogin';
import { login } from '../../store/actions/accountActions';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import { Button, Grid, Header, Message, Segment } from 'semantic-ui-react';

export default function Login() {

    const logIn = useLogin();

    const dispatch = useDispatch()

    const navigate = useNavigate();

    const handleSubmit = async (values) => {
        const result = await logIn(values);

        if (result.isSuccess) {
            dispatch(login({ name: result.data.userName }));
            navigate("/");
        }
    }

    const loginSchema = Yup.object().shape({
        email: Yup.string()
            .required("Email is a required field")
            .email("Invalid email format"),
        password: Yup.string()
            .required("Password is a required field")
            .matches(
                /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,})/,
                "Must Contain 8 Characters, One Uppercase, One Lowercase, One Number"
            )
    });

    return (
        <Grid textAlign='center' style={{ height: '60vh' }} verticalAlign='middle'>
            <Grid.Column style={{ maxWidth: 450 }}>
                <Header as='h2' color='teal' textAlign='center'>
                    Log-in to your account
                </Header>
                <Segment stacked>

                    <Formik
                        initialValues={{ email: '', password: '' }}
                        validationSchema={loginSchema}
                        onSubmit={handleSubmit}
                    >
                        <Form className='m-3'>
                            <div className="form-group p-2">
                                <label htmlFor="email">Email</label>
                                <Field name="email" type="text" className='form-control' />
                                <ErrorMessage name="email" component='div' style={{ color: 'red' }} />
                            </div>
                            <div className="form-group p-2 mb-3">
                                <label htmlFor="password">Password</label>
                                <Field name="password" type="password" className='form-control' />
                                <ErrorMessage name="password" component='div' style={{ color: 'red' }} />
                            </div>

                            <Button color='teal' fluid size='large' type='submit'>
                                Login
                            </Button>
                        </Form>
                    </Formik>

                </Segment>
                <Message>
                    New to us? <Link to='/register' >Register</Link>
                </Message>
            </Grid.Column>
        </Grid>
    )
}