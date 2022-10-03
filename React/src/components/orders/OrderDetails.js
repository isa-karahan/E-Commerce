import { Grid, Header, Table, Button, Card, Icon, Loader } from "semantic-ui-react";
import useCancelOrder from '../../hooks/orderService/useCancelOrder';
import useGetOrders from '../../hooks/orderService/useGetOrders';

export default function OrderDetails() {

	const [response, error, loading, refetch] = useGetOrders();

	const cancelOrder = useCancelOrder();

	const handleCancelOrder = async (orderId) => {

		const result = await cancelOrder({ orderId: orderId });

		if (result.isSuccess) {
			refetch();
		}
	}

	return (
		<Grid textAlign='center' style={{ height: '40vh' }} verticalAlign='middle'>
			<Grid.Column style={{ maxWidth: 750 }}>
				<Header as='h2' color='teal' textAlign='center' className="mb-5">
					My Orders
				</Header>
				<Loader active={loading} size='big' className="mt-5" />

				<Card.Group centered>
					{
						response?.length === 0 && !loading && !error ?
							<Header as='h3' className='m-5'>You have not ordered anything yet.</Header>
							:
							response?.map(
								order => {
									return <Card fluid key={order.orderDetailId} color='black'>

										<Table celled compact color='green'>
											<Table.Header fullWidth>
												<Table.Row>
													<Table.Cell colSpan='2'><strong>Order Date</strong></Table.Cell>
													<Table.Cell textAlign="center" colSpan='4'>{order.orderDate}</Table.Cell>
												</Table.Row>
												<Table.Row>
													<Table.Cell colSpan='2'><strong>Order Address</strong></Table.Cell>
													<Table.Cell textAlign="center" colSpan='4'>{order.orderAddress}</Table.Cell>
												</Table.Row>
												<Table.Row>
													<Table.Cell colSpan='2'><strong>Total Price</strong></Table.Cell>
													<Table.Cell textAlign="center" colSpan='4'>{order.totalPrice} $</Table.Cell>
												</Table.Row>
											</Table.Header>

										</Table>

										<Table celled compact striped color="teal">
											<Table.Header fullWidth>
												<Table.Row>
													<Table.HeaderCell width={10}>Name</Table.HeaderCell>
													<Table.HeaderCell>Unit Price</Table.HeaderCell>
													<Table.HeaderCell>Quantity</Table.HeaderCell>
												</Table.Row>
											</Table.Header>

											<Table.Body>
												{
													order.orderItems.map(
														item => <Table.Row key={item.productId}>
															<Table.Cell>{item.productName}</Table.Cell>
															<Table.Cell>{item.unitPrice} $</Table.Cell>
															<Table.Cell>{item.quantity}</Table.Cell>
														</Table.Row>
													)
												}
											</Table.Body>

											<Table.Footer fullWidth>
												<Table.Row>
													<Table.HeaderCell colSpan='4'>
														<Button
															floated='right'
															icon
															labelPosition='left'
															color="red"
															onClick={() => handleCancelOrder(order.orderDetailId)}
														>
															<Icon inverted name='cancel' /> Cancel Order
														</Button>
													</Table.HeaderCell>
												</Table.Row>
											</Table.Footer>
										</Table>

									</Card>
								}
							)
					}
				</Card.Group>
			</Grid.Column>
		</Grid>
	)
}
