import { Grid, Header, Table, Input } from "semantic-ui-react";
import { useState } from "react";
import useGetWallet from '../../hooks/userService/useGetWallet';
import useDepositMoney from '../../hooks/userService/useDepositMoney';

export default function Wallet() {

	const [response, error, loading, refetch] = useGetWallet();

	const depositMoney = useDepositMoney();

	const [amount, setAmount] = useState(10);

	const handleDepositMoney = async () => {

		await depositMoney({ amount: amount });
		refetch();
	}

	return (
		<Grid textAlign='center' style={{ height: '40vh' }} verticalAlign='middle'>
			<Grid.Column style={{ maxWidth: 750 }}>
				<Header as='h2' color='teal' textAlign='center' className="mb-5">
					My Wallet
				</Header>

				{
					response && !error && !loading &&
					<>
						<Table celled compact color='green' verticalAlign="middle">
							<Table.Header fullWidth>
								<Table.Row verticalAlign="middle">
									<Table.Cell><strong>Balance</strong></Table.Cell>
									<Table.Cell>{response.balance} $</Table.Cell>
									<Table.Cell collapsing>
										<Input
											action={{
												color: 'teal',
												labelPosition: 'right',
												icon: 'credit card outline',
												content: 'Deposit',
												onClick: handleDepositMoney
											}}
											defaultValue={amount}
											onChange={(e, { value }) => setAmount(value)}
										/>
									</Table.Cell>
								</Table.Row>
							</Table.Header>
						</Table>

						{
							response?.transactions.length === 0 ?
								<Header className="mt-5">No transaction available.</Header>
								:
								<Table basic striped color="teal">
									<Table.Header>
										<Table.Row>
											<Table.HeaderCell>Date</Table.HeaderCell>
											<Table.HeaderCell>Amount</Table.HeaderCell>
											<Table.HeaderCell>Type</Table.HeaderCell>
										</Table.Row>
									</Table.Header>

									<Table.Body>
										{
											response?.transactions.map(
												transaction => <Table.Row key={transaction.id}>
													<Table.Cell>{transaction.date}</Table.Cell>
													<Table.Cell>{transaction.amount} $</Table.Cell>
													<Table.Cell>{transaction.type}</Table.Cell>
												</Table.Row>
											)
										}
									</Table.Body>

								</Table>
						}
					</>
				}

			</Grid.Column>
		</Grid>
	)
}
