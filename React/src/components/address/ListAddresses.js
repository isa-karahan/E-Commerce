import { Link } from 'react-router-dom';
import useGetAddresses from '../../hooks/userService/useGetAddresses';
import useDeleteAddress from '../../hooks/userService/useDeleteAddress';
import { Table, Button, Header, Icon, Grid, Loader } from 'semantic-ui-react';

export default function ListAddressesWithDelete() {

    const [response, error, loading, refetch] = useGetAddresses();

    const deleteAddress = useDeleteAddress();

    const handleDeleteAddress = async (choosenAddress) => {

        await deleteAddress({ addressId: choosenAddress });

        refetch();
    }

    const addAddressButton = <Link to='/addresses/add'>
        <Button
            icon
            labelPosition='left'
            color='green'
        >
            <Icon name='remove' size='big' />
            Add Address
        </Button>
    </Link>

    return (
        <Grid textAlign='center' style={{ height: '40vh' }} verticalAlign='middle'>
            <Grid.Column style={{ maxWidth: 750 }}>

                <Loader active={loading} size='big' className="mt-5" />

                {
                    (response?.length === 0 && loading) || error ?
                        <>
                            <Header>No address found</Header>
                            <div className='m-5'>
                                {addAddressButton}
                            </div>
                        </>
                        : <>
                            <Header as='h2' color='teal' textAlign='center'>
                                Addresses
                            </Header>
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
                                        response.map(
                                            address => <Table.Row key={address.id}>
                                                <Table.Cell verticalAlign="middle"><strong>{address.name}</strong></Table.Cell>
                                                <Table.Cell verticalAlign="middle">
                                                        {`${address.street} ${address.postCode} - ${address.state}/${address.city}/${address.country}`}
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Button
                                                        floated='right'
                                                        icon
                                                        labelPosition='left'
                                                        color='red'
                                                        onClick={() => handleDeleteAddress(address.id)}
                                                    >
                                                        <Icon name='remove' size='big' /> Delete
                                                    </Button>
                                                </Table.Cell>
                                            </Table.Row>
                                        )
                                    }
                                </Table.Body>

                                <Table.Footer fullWidth>
                                    <Table.Row>
                                        <Table.HeaderCell colSpan='4'>
                                            {addAddressButton}
                                        </Table.HeaderCell>
                                    </Table.Row>
                                </Table.Footer>

                            </Table>
                        </>
                }

            </Grid.Column>
        </Grid>
    )
}
