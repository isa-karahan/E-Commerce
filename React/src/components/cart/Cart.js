import { toast } from 'react-toastify';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { removeFromCart } from '../../store/actions/cartActions';
import useGetProducts from '../../hooks/productService/useGetProducts';
import { Button, Icon, Table, Grid, Header, Loader } from 'semantic-ui-react'

export default function Cart() {

	const [response, error, loading] = useGetProducts();

	const { cartItems } = useSelector(state => state);

	const [cart, setCart] = useState([]);
	const [price, setPrice] = useState(0);

	const dispatch = useDispatch();

	const navigate = useNavigate();

	useEffect(() => {

		if(!loading){
			cartItems.forEach(item => {
				const product = response?.find(p => p.id === item.product);
	
				const details = {
					product: item.product,
					quantity: item.quantity,
					name: product?.name,
					unitPrice: product?.unitPrice
				};
	
				setPrice(price => price + product?.unitPrice * item.quantity);
				setCart(cart => [...cart, details]);
			});
		}

		// eslint-disable-next-line
	}, [response, cartItems])

	const handleRemoveFromCart = async (cartItem) => {
		dispatch(removeFromCart({
			product: cartItem.product,
		}));
		setCart([]);
		setPrice(0);
		toast.success(`${cartItem.name} removed from the cart.`);
	}

	const handleCompleteOrder = async () => {
		if (cartItems.length === 0) {
			return toast.error('Please add product to the cart!');
		}
		navigate('/order/address');
	}

	return (
		<Grid textAlign='center' style={{ height: '40vh' }} verticalAlign='middle'>
			<Grid.Column style={{ maxWidth: 750 }}>
				<Header as='h2' color='teal' textAlign='center'>
					My Cart
				</Header>

				<Loader active={loading} className='mt-5'/>

				{
					cart.length === 0 && !error && !loading ?
						<Header as='h3' className='m-5'>Cart is empty.</Header>
						:
						<Table celled compact>
							<Table.Header fullWidth>
								<Table.Row>
									<Table.HeaderCell>Name</Table.HeaderCell>
									<Table.HeaderCell>Unit Price</Table.HeaderCell>
									<Table.HeaderCell>Quantity</Table.HeaderCell>
									<Table.HeaderCell collapsing />
								</Table.Row>
							</Table.Header>

							<Table.Body>
								{
									cart.map(
										item => <Table.Row key={item.product}>
											<Table.Cell>{item.name}</Table.Cell>
											<Table.Cell>{item.unitPrice}</Table.Cell>
											<Table.Cell>{item.quantity}</Table.Cell>
											<Table.Cell>
												<Button color='red' onClick={() => handleRemoveFromCart(item)}>
													Remove
												</Button>
											</Table.Cell>
										</Table.Row>
									)
								}
							</Table.Body>

							<Table.Footer fullWidth>
								<Table.Row>
									<Table.HeaderCell colSpan='3'><strong>Total Price: {price} $</strong></Table.HeaderCell>
									<Table.HeaderCell colSpan='4'>
										<Button
											floated='right'
											icon
											labelPosition='left'
											primary
											onClick={handleCompleteOrder}
										>
											<Icon name='angle right' size='big' /> Complete Order
										</Button>
									</Table.HeaderCell>
								</Table.Row>
							</Table.Footer>
						</Table>
				}

			</Grid.Column>
		</Grid>
	)
}
