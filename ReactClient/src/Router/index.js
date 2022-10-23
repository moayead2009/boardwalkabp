import React from 'react'
import { Route, Switch } from 'react-router-dom'
import Router from 'react-router-dom/BrowserRouter';
import App from '@/components/App'
import AdminRegister from '@/components/Admins/AdminRegister'
import AdminLogin from '@/components/Admins/AdminLogin'
// import UserRegister from '@/components/Users/UserRegister'
// import UserLogin from '@/components/Users/UserLogin'
// import ClientRegister from '@/components/Clients/ClientRegister'
// import ClientLogin from '@/components/Clients/ClientLogin'
// import Admins from '@/components/Admins/Admins'



const Routes = () => (
    <Router>
        <Switch>
            <Route exact path="/" component={App} />
            <Route exact path="/AdminRegister" component={AdminRegister} />
            <Route exact path="/AdminLogin" component={AdminLogin} />
        </Switch>
    </Router>
);

export default Routes;