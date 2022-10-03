import * as Yup from 'yup';
import { useNavigate } from 'react-router-dom';
import useAddAddress from '../../hooks/userService/useAddAddress';
import { Grid, Button, Header } from 'semantic-ui-react';
import { Formik, Field, ErrorMessage, useField, Form } from 'formik';

export default function AddAddress() {

	const addAddress = useAddAddress();

	const navigate = useNavigate();

	const handleAddAddress = async (values) => {

		const result = await addAddress(values);

		if (result.isSuccess) {
			navigate('/addresses');
		}
	}

	const initialValues = {
		name: '', country: '', city: '', state: '', street: '', postCode: ''
	};

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

	const addressSchema = Yup.object().shape({
		name: Yup.string().required('Name is required'),
		country: Yup.string().required('Country is required'),
		city: Yup.string().required('City is required'),
		state: Yup.string().required('State is required'),
		street: Yup.string().required('Street is required'),
		postCode: Yup.string().required('Post Code is required')
			.min(5, 'Post Code must be at least 5 characters')
	});

	return (
		<Grid textAlign='center' style={{ height: '40vh' }} verticalAlign='middle'>
			<Grid.Column style={{ maxWidth: 650 }}>
				<Header as='h2' color='teal' textAlign='center' className='m-5'>
					Add Address
				</Header>

				<Formik
					initialValues={initialValues}
					validationSchema={addressSchema}
					onSubmit={handleAddAddress}
				>
					<Form>
						<div className='parent'>
							<Input name='name' label='Name' type='text' />
							<Input name='country' label='Country' type='text' />
						</div>

						<div className='parent'>
							<Input name='city' label='City' type='text' />
							<Input name='state' label='State' type='text' />
						</div>

						<div className='parent'>
							<Input name='street' label='Street' type='text' />
							<Input name='postCode' label='Post Code' type='text' />
						</div>

						<Button primary fluid type='submit'>Add</Button>
					</Form>
				</Formik>

			</Grid.Column>
		</Grid>
	)
}
