import { useNavigate } from "react-router-dom";
import useGetFavorites from '../../hooks/userService/useGetFavorites';
import useDeleteFavorite from '../../hooks/userService/useDeleteFavorite';
import { Button, Icon, Table, Grid, Header, Loader } from 'semantic-ui-react'

export default function FavoritesList() {

	const navigate = useNavigate();

	const deleteFavorite = useDeleteFavorite();

	const [response, error, loading, refetch] = useGetFavorites();

	const handleRemove = async (productId) => {
		await deleteFavorite({ productId: productId });

		refetch();
	}

	return (
		<Grid textAlign='center' style={{ height: '40vh' }} verticalAlign='middle'>
			<Grid.Column style={{ maxWidth: 750 }}>
				<Header as='h2' color='teal' textAlign='center'>
					Favorites List
				</Header>

				<Loader active={loading} size='big' className="mt-5" />

				{
					response && !error && !loading && <Table striped verticalAlign="middle">
						<Table.Header>
							<Table.Row>
								<Table.HeaderCell>Product Name</Table.HeaderCell>
								<Table.HeaderCell>Unit Price</Table.HeaderCell>
								<Table.HeaderCell>Added</Table.HeaderCell>
								<Table.HeaderCell collapsing></Table.HeaderCell>
							</Table.Row>
						</Table.Header>

						<Table.Body>
							{
								response?.map(
									favorite =>
										<Table.Row key={favorite.id}>
											<Table.Cell
												onClick={() => navigate(`/products/${favorite.productId}`)}
											>
												{favorite.productName}
											</Table.Cell>
											<Table.Cell
												onClick={() => navigate(`/products/${favorite.productId}`)}
											>
												{favorite.unitPrice}
											</Table.Cell>
											<Table.Cell
												onClick={() => navigate(`/products/${favorite.productId}`)}
											>
												{favorite.added}
											</Table.Cell>
											<Table.Cell>
												<Button
													labelPosition='left'
													color='red'
													floated="right"
													icon
													onClick={() => handleRemove(favorite.productId)}
												>
													<Icon name="remove"></Icon> Remove
												</Button>
											</Table.Cell>
										</Table.Row>
								)
							}
						</Table.Body>
					</Table>
				}

			</Grid.Column>
		</Grid>
	)
}
