import { Provider } from 'react-redux';
import ReactDOM from 'react-dom/client';
import App from './components/root/App';
import { getStore } from './store/getStore';
import { ToastContainer } from 'react-toastify';
import history from './components/history/history';
import { unstable_HistoryRouter as HistoryRouter } from 'react-router-dom';

import './index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'react-toastify/dist/ReactToastify.min.css';

const root = ReactDOM.createRoot(document.getElementById('root'));
const store = getStore();

root.render(
    <Provider store={store}>
        <ToastContainer position='bottom-right' />
        <HistoryRouter history={history}>
            <App />
        </HistoryRouter>
    </Provider>
);
