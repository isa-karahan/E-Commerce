import useLogout from '../../hooks/authService/useLogout';
import { Menu, Button, Icon, Dropdown } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { logout } from '../../store/actions/accountActions';
import { useNavigate } from 'react-router-dom';

export default function AccountIcons() {

    const logOut = useLogout();

    const { user } = useSelector(state => state);

    const dispatch = useDispatch();

    const navigate = useNavigate();

    const handleLogOut = async () => {

        await logOut();

        dispatch(logout(user));

        navigate('/');
    }

    return user ?
        <div className='m-1'>
            <Menu.Menu position='right'>
                <Menu.Item>
                    <Icon name='user circle' size='big' />
                    <Dropdown text={user.name} options={[
                        { key: 'Profile', text: 'Profile', as: Link, to: '/profile' },
                        { key: 'Orders', text: 'Orders', as: Link, to: '/orders' },
                        { key: 'Wallet', text: 'Wallet', as: Link, to: '/wallet' },
                        { key: 'Addresses', text: 'Addresses', as: Link, to: '/addresses' },
                        { key: 'Logout', text: 'Logout', as: Button, onClick: handleLogOut }
                    ]} simple item />

                </Menu.Item>
            </Menu.Menu>
        </div>
        :
        <div className='categories-header'>
            <Menu.Menu position='right'>
                <Menu.Item>
                    <Button color='green' as={Link} to='/register'>Register</Button>
                </Menu.Item>

                <Menu.Item>
                    <Button color='red' as={Link} to='/login'>Log-in</Button>
                </Menu.Item>
            </Menu.Menu>
        </div>
}
