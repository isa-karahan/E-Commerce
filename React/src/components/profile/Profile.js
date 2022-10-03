import { useState } from "react";
import { useNavigate } from "react-router-dom";
import useGetProfile from '../../hooks/userService/useGetProfile';
import useDeleteAccount from '../../hooks/authService/useDeleteAccount';
import useUpdateProfile from '../../hooks/userService/useUpdateProfile';
import useChangePassword from '../../hooks/authService/useChangePassword';
import { Grid, Segment, Header, Table, Button, Icon, Input, Form } from "semantic-ui-react";

export default function Profile() {

	const [response, error, loading, refetch] = useGetProfile();

	const updateProfile = useUpdateProfile();
	const deleteAccount = useDeleteAccount();
	const changePassword = useChangePassword();

	const [profileEditMode, setProfileEditMode] = useState(false);
	const [updatePasswordMode, setUpdatePasswordMode] = useState(false);

	const [form, setForm] = useState({});

	const navigate = useNavigate();

	const handleChange = (e, { name, value }) => setForm({ ...form, [name]: value })

	const handleUpdateProfile = async () => {
		const result = await updateProfile(form);

		if (result.isSuccess) {
			setForm({});
			setProfileEditMode(false);
			refetch();
		}
	}

	const handleDeleteAccount = async () => {
		const result = await deleteAccount();

		if (result.isSuccess) {
			localStorage.clear();
			navigate('/');
		}
	}

	const handleUpdatePassword = async () => {

		const result = await changePassword(form);

		if (result.isSuccess) {
			setForm({});
			setUpdatePasswordMode(false);
		}
	}

	return (
		<>
			<Header as='h2' textAlign="center" color="teal">
				Profile
			</Header>

			<Grid container textAlign="center" style={{ marginTop: "50px" }}>
				<Segment
					style={{ height: "400px", width: "800px" }}
					textAlign="left" vertical
				>

					{
						response && !loading && !error &&
						<>
							<Button.Group fluid>
								<Button
									labelPosition="left"
									color='red'
									className="m-3"
									icon
									onClick={handleDeleteAccount}
								>
									<Icon name='remove' size="large" /> Delete Account
								</Button>
								<Button
									labelPosition="left"
									color='green'
									className="m-3"
									icon
									onClick={() => {
										setUpdatePasswordMode(!updatePasswordMode);
										setForm({});
									}}
								>
									<Icon name='key' size="large" /> Update Password
								</Button>
								<Button
									labelPosition="left"
									color="blue"
									className="m-3"
									icon
									onClick={() => {
										setProfileEditMode(!profileEditMode);
										setForm({
											firstName: response.firstName,
											lastName: response.lastName,
											phoneNumber: response.phoneNumber
										});
									}}
								>
									<Icon name='pencil alternate' size="large" /> Edit Profile
								</Button>
							</Button.Group>

							{
								updatePasswordMode && <Form className="m-5">
									<Form.Group widths='equal'>
										<Form.Field>
											<label>Current Password</label>
											<Form.Input
												required
												placeholder='***'
												type="password"
												name='currentPassword'
												onChange={handleChange}
											/>
										</Form.Field>
										<Form.Field>
											<label>New Password</label>
											<Form.Input
												required
												placeholder='***'
												type="password"
												name='newPassword'
												onChange={handleChange}
											/>
										</Form.Field>
										<Form.Field className="mt-4">
											<Form.Button className="mt-1" primary onClick={handleUpdatePassword}>Update</Form.Button>
										</Form.Field>
									</Form.Group>
								</Form>

							}

							<Table basic='very' celled verticalAlign="middle">

								<Table.Body>
									<Table.Row>
										<Table.Cell>
											<Header as='h4'>
												<Header.Content>
													First Name
												</Header.Content>
											</Header>
										</Table.Cell>
										<Table.Cell>
											{
												profileEditMode ?
													<Input focus
														name='firstName'
														value={form.firstName}
														onChange={handleChange} />
													:
													response.firstName
											}
										</Table.Cell>
									</Table.Row>
									<Table.Row>
										<Table.Cell>
											<Header as='h4'>
												<Header.Content>
													Last Name
												</Header.Content>
											</Header>
										</Table.Cell>
										<Table.Cell>
											{
												profileEditMode ?
													<Input focus
														name='lastName'
														value={form.lastName}
														onChange={handleChange} />
													:
													response.lastName
											}
										</Table.Cell>
									</Table.Row>
									<Table.Row>
										<Table.Cell >
											<Header as='h4' >
												<Header.Content>
													Email
												</Header.Content>
											</Header>
										</Table.Cell>
										<Table.Cell>{response.email}</Table.Cell>
									</Table.Row>
									<Table.Row>
										<Table.Cell>
											<Header as='h4' >
												<Header.Content>
													Phone Number
												</Header.Content>
											</Header>
										</Table.Cell>
										<Table.Cell>
											{
												profileEditMode ?
													<Input focus
														name='phoneNumber'
														value={form.phoneNumber}
														onChange={handleChange} />
													:
													response.phoneNumber
											}
										</Table.Cell>
									</Table.Row>
									<Table.Row>
										<Table.Cell>
											<Header as='h4' >
												<Header.Content>
													Membership Date
												</Header.Content>
											</Header>
										</Table.Cell>
										<Table.Cell>{response.added}</Table.Cell>
									</Table.Row>
								</Table.Body>
							</Table>
						</>
					}



				</Segment>
			</Grid>

			{profileEditMode && <Button className="m-4" primary circular onClick={handleUpdateProfile}>Update Profile</Button>}
		</>
	)
}
