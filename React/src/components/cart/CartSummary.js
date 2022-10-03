import { Menu, Label, Icon } from 'semantic-ui-react';
import { useSelector } from 'react-redux';
import { Link } from 'react-router-dom';

export default function CartSummary() {

    const { cartItems } = useSelector(state => state);

    return (
        <div className='m-3'>
            <Link to='/cart'>
                <Menu size='mini' inverted color='teal'>
                    <Menu.Item>
                        <Icon name='cart' size='large'/>
                        <Label color='yellow'>{cartItems.length}</Label>
                    </Menu.Item>
                </Menu>
            </Link>
        </div>
    )
}
