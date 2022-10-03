import { useState } from 'react';
import { Col, Row } from 'reactstrap';
import AddToCart from '../cart/AddToCart';
import AddToFavorites from '../favorites/AddToFavorites';
import { useSelector } from 'react-redux';
import useAddComment from '../../hooks/productService/useAddComment';
import useGetProduct from '../../hooks/productService/useGetProduct';
import { Image, Button, Card, Icon, Label, Form, Grid } from 'semantic-ui-react';

export default function ProductDetail() {

    const [response, error, loading, refetch] = useGetProduct();

    const { user } = useSelector(state => state);

    const addComment = useAddComment();

    const [commentText, setCommentText] = useState('');

    const [quantity, setQuantity] = useState(1);

    const decreaseQuantity = () => {
        if (quantity > 1) setQuantity(quantity - 1);
    }

    const increaseQuantity = () => {
        setQuantity(quantity + 1);
    }

    const handleAddComment = async () => {
        await addComment({
            productId: response.id,
            text: commentText
        });

        setCommentText('');
        refetch();
    }

    return (
        <Grid textAlign='center' style={{ height: '40vh' }} verticalAlign='middle'>
            <Grid.Column style={{ maxWidth: 1200 }}>

                {
                    response && !error && !loading && <>
                        <Row className='show-grid'>
                            <Col md={4}>
                                <Image
                                    width={300} height={300}
                                    src='https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSRsyL8RHI76mQheyv4TViIng30EF0UfNAc4A&usqp=CAU'
                                />
                            </Col>
                            <Col md={7}>
                                <div>
                                    <h1>{response.name}</h1>
                                    <h3>{response.unitPrice} $</h3>
                                    <h5>% {response.bonusPercentage} Cashback</h5>
                                </div>
                                {
                                    user && <div className='m-5'>
                                        <AddToFavorites product={response.id}></AddToFavorites>

                                        <Button.Group icon>
                                            <Button onClick={decreaseQuantity}>
                                                <Icon name='minus' />
                                            </Button>
                                            <Label><h4>{quantity}</h4></Label>
                                            <Button onClick={increaseQuantity}>
                                                <Icon name='plus' />
                                            </Button>
                                        </Button.Group>
                                        <AddToCart product={response} quantity={quantity} ></AddToCart>
                                    </div>
                                }

                                <h4>{response.description}</h4>
                            </Col>
                        </Row>
                        <Card.Group className='m-5'>
                            <Card fluid>
                                <Card.Header><h3>Commments</h3></Card.Header>
                            </Card>
                            {
                                response?.comments.map(
                                    comment =>
                                        <Card color='blue' fluid key={comment.id}>
                                            <Card.Header className='p-1'><h4>{comment.userName} - {comment.added}</h4></Card.Header>
                                            <Card.Description textAlign='left' className='p-3'>{comment.text}</Card.Description>
                                        </Card>
                                )
                            }

                        </Card.Group>

                        {
                            user && <Form >
                                <Form.TextArea onChange={(e, { value }) => setCommentText(value)} value={commentText} width={16} />
                                <Button content='Add Comment' labelPosition='left' icon='edit' primary onClick={handleAddComment} />
                            </Form>
                        }

                    </>
                }

            </Grid.Column>
        </Grid>
    )
}
