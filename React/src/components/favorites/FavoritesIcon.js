import { Menu, Label, Icon } from 'semantic-ui-react';
import { Link } from 'react-router-dom';

export default function FavoritesIcon() {

    return (
        <div className='m-3'>
            <Link to='/favorites'>
                <Menu size='mini' inverted color='red'>
                    <Menu.Item>
                        <Icon name='heart' size='large'/>
                        <Label color='red'>Favorites</Label>
                    </Menu.Item>
                </Menu>
            </Link>
        </div>
    )
}
