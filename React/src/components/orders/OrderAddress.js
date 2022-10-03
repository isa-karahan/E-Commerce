import { toast } from "react-toastify";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useSelector, useDispatch } from 'react-redux';
import { removeFromCart } from "../../store/actions/cartActions";
import useCreateOrder from '../../hooks/orderService/useCreateOrder';
import useGetAddresses from '../../hooks/userService/useGetAddresses';
import { Segment, Table, Icon, Button, Radio, Grid, Header } from 'semantic-ui-react';

export default function OrderAddress() {

    const [response, error, loading] = useGetAddresses();

    const createOrder = useCreateOrder();

    const [choosenAddress, setChoosenAddress] = useState(null);

    const dispatch = useDispatch();

    const { cartItems } = useSelector(state => state);

    const navigate = useNavigate();

    useEffect(() => {

        if (!loading) {
            if (response.length === 0) {
                toast.error('Please add address to continue...')
                return navigate('/addresses/add');
            }

            setChoosenAddress(response.at(0).id);
        }

        // eslint-disable-next-line
    }, [])

    const handleChange = async (e, { value }) => setChoosenAddress(value);

    const handleGiveOrder = async () => {

        const result = await createOrder({
            addressId: choosenAddress,
            orderItems: cartItems
        });


        if (result.isSuccess) {
            for await (let item of cartItems) {
                dispatch(removeFromCart({
                    product: item.product,
                }));
            }

            navigate('/orders');
        }
    }

    return (
        <Grid textAlign='center' style={{ height: '40vh' }} verticalAlign='middle'>
            <Grid.Column style={{ maxWidth: 750 }}>
                <Header as='h2' color='teal' textAlign='center'>
                    Choose Address
                </Header>

                {
                    response && !error && !loading &&
                    <Table celled compact>
                        <Table.Header fullWidth>
                            <Table.Row>
                                <Table.HeaderCell>Name</Table.HeaderCell>
                                <Table.HeaderCell>Description</Table.HeaderCell>
                                <Table.HeaderCell collapsing />
                            </Table.Row>
                        </Table.Header>

                        <Table.Body>
                            {
                                response?.map(
                                    address => <Table.Row key={address.id}>
                                        <Table.Cell verticalAlign="middle">{address.name}</Table.Cell>
                                        <Table.Cell verticalAlign="middle">
                                            <strong>
                                                {`${address.street} ${address.postCode} - ${address.state}/${address.city}/${address.country}`}
                                            </strong>
                                        </Table.Cell>
                                        <Table.Cell>
                                            <Segment compact>
                                                <Radio
                                                    value={address.id}
                                                    checked={choosenAddress === address.id}
                                                    onChange={handleChange}
                                                />
                                            </Segment>
                                        </Table.Cell>
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
                                        primary
                                        onClick={handleGiveOrder}
                                    >
                                        <Icon name='angle right' size='big' /> Give Order
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
