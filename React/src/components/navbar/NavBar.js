import { Link } from 'react-router-dom';
import CartSummary from '../cart/CartSummary';
import Categories from '../categories/Categories';
import AccountIcons from '../account/AccountIcons';
import FavoritesIcon from '../favorites/FavoritesIcon';
import { useDispatch, useSelector } from 'react-redux';
import { changeServer } from '../../store/actions/serverActions';
import { NodeJsURL, dotNetURL } from '../../apiClient/serverURLs';
import { Menu, Container, Radio, Label } from 'semantic-ui-react';

export default function Navbar() {

    const dispatch = useDispatch();

    const user = useSelector(state => state.user);

    const handleServerChange = async (_, { checked }) => {

        const url = checked ? dotNetURL : NodeJsURL;

        dispatch(changeServer(url))
    }

    return (
        <div>
            <Menu inverted color='blue' size='small'>
                <Container>
                    <Menu.Menu position='left'>
                        <Menu.Item
                            name='home' as={Link} to='/'
                        ></Menu.Item>
                        <Menu.Item>
                            <Label className='m-3'>Node</Label>
                            <Radio slider onChange={handleServerChange} />
                            <Label>.NET</Label>
                        </Menu.Item>
                        <Menu.Item>
                            <Categories />
                        </Menu.Item>
                    </Menu.Menu>

                    {
                        user && (
                            <>
                                <FavoritesIcon />
                                <CartSummary />
                            </>
                        )
                    }
                    <AccountIcons />
                </Container>
            </Menu>
        </div>
    )
}