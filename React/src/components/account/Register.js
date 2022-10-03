import * as Yup from 'yup';
import { useNavigate } from 'react-router-dom';
import { Grid, Button, Header, Segment } from 'semantic-ui-react';
import { Formik, Field, Form, ErrorMessage, useField } from 'formik';
import useRegister from '../../hooks/authService/useRegister';

export default function Register() {

    const register = useRegister();

    const navigate = useNavigate();

    const initialValues = {
        firstName: '',
        lastName: '',
        phoneNumber: '',
        email: '',
        password: '',
        confirmPassword: '',
        acceptTerms: false,
    };

    const handleSubmit = async (values) => {
        const temp = { ...values };
        delete temp.confirmPassword;
        delete temp.acceptTerms;
        const result = await register(temp);

        if (result.isSuccess) {
            navigate("/login");
        }
    }

    const Input = ({ name, label, type }) => {
        const [field] = useField(name);
        return (
            <div className="form-group m-3">
                <label htmlFor={field.name}>{label}</label>
                <Field label={field.name} name={field.name} type={type} className="form-control" />
                <ErrorMessage
                    name={field.name}
                    component="div"
                    className="text-danger"
                />
            </div>
        );
    };

    const SignUpSchema = Yup.object().shape({
        firstName: Yup.string().required('First Name is required'),
        lastName: Yup.string()
            .required('Last Name is required')
            .min(6, 'Last Name must be at least 6 characters')
            .max(20, 'Last Name must not exceed 20 characters'),
        phoneNumber: Yup.string().matches(/^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/, "Please enter valid phone number."),
        email: Yup.string()
            .required('Email is required')
            .email('Email is invalid'),
        password: Yup.string()
            .required('Password is required')
            .matches(
                /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,})/,
                "Must Contain 8 Characters, One Uppercase, One Lowercase, One Number"
            ),
        confirmPassword: Yup.string()
            .required('Confirm Password is required')
            .oneOf([Yup.ref('password'), null], 'Confirm Password does not match'),
        acceptTerms: Yup.bool().oneOf([true], 'Accept Terms is required'),
    });

    return (
        <Grid textAlign='center' style={{ height: '40vh' }} verticalAlign='middle'>
            <Grid.Column style={{ maxWidth: 650 }}>
                <Header as='h2' color='teal' textAlign='center'>
                    Create a new account
                </Header>

                <Segment stacked>
                    <div className="register-form p-3">
                        <Formik
                            initialValues={initialValues}
                            validationSchema={SignUpSchema}
                            onSubmit={handleSubmit}
                        >
                            <Form>
                                <Input name='firstName' label='First Name' type='text' />
                                <Input name='lastName' label='Last Name' type='text' />
                                <Input name='phoneNumber' label='Phone Number' type='text' />
                                <Input name='email' label='Email' type='text' />
                                <Input name='password' label='Password' type='password' />
                                <Input name='confirmPassword' label='Confirm Password' type='password' />


                                <div className="form-group form-check m-3">
                                    <Field
                                        name="acceptTerms"
                                        type="checkbox"
                                        className="form-check-input"
                                    />
                                    <label htmlFor="acceptTerms" className="form-check-label">
                                        I have read and agree to the Terms
                                    </label>
                                    <ErrorMessage
                                        name="acceptTerms"
                                        component="div"
                                        className="text-danger"
                                    />
                                </div>

                                <Button primary fluid type='submit'>Register</Button>
                            </Form>
                        </Formik>
                    </div>
                </Segment>

            </Grid.Column>
        </Grid>
    )
}
