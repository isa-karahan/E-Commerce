import { Container } from 'semantic-ui-react';
import NavBar from '../navbar/NavBar';
import { Route, Routes } from 'react-router-dom';
import Products from '../products/Products';
import ProductDetail from '../products/ProductDetail';
import Register from '../account/Register';
import Login from '../account/Login';
import Wallet from '../wallet/Wallet';
import Profile from '../profile/Profile';
import FavoritesList from '../favorites/FavoritesList';
import Cart from '../cart/Cart';
import OrderDetails from '../orders/OrderDetails';
import OrderAddress from '../orders/OrderAddress';
import AddAddress from '../address/AddAddress';
import ListAddresses from '../address/ListAddresses';
import ErrorBoundary from '../error/ErrorBoundary';

function App() {
	return (
		<div>
			<NavBar></NavBar>
			<Container className='box'>
				<ErrorBoundary>

					<Routes>
						<Route exact path='/' element={<Products />}></Route>
						<Route exact path='/categories/:categoryId' element={<Products />}></Route>
						<Route exact path='/products/:id' element={<ProductDetail />}></Route>

						<Route exact path='/register' element={<Register />}></Route>
						<Route exact path='/login' element={<Login />}></Route>

						<Route exact path='/wallet' element={<Wallet />}></Route>

						<Route exact path='/addresses' element={<ListAddresses />}></Route>
						<Route exact path='/addresses/add' element={<AddAddress />}></Route>

						<Route exact path='/profile' element={<Profile />}></Route>

						<Route exact path='/orders' element={<OrderDetails />}></Route>
						<Route exact path='/order/address' element={<OrderAddress />}></Route>

						<Route exact path='/favorites' element={<FavoritesList />}></Route>

						<Route exact path='/cart' element={<Cart />}></Route>

					</Routes>
				</ErrorBoundary>
			</Container>
		</div>
	);
}

export default App;
