import './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Login from './components/Login';
import Register from './components/Register';
import Home from './components/Home';
import Applications from './components/Applications';
import Categories from './components/Categories';
import Clients from './components/Clients';
import Questions from './components/Questions';
import Layout from './components/Layout';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/register" exact element={<Register />} />
        <Route path="/" exact element={<Login />} />
        <Route path="*" exact element={<h1>404 Not Found</h1>} />

        <Route path="/" exact element = {<Layout />}>
          <Route path="/home" exact element={<Home />} />
          <Route path="/applications" exact element={Applications} />
          <Route path="/categories" exact element={Categories} />
          <Route path="/clients" exact element={Clients} />
          <Route path="/questions" exact element={Questions} />
          
        </Route>
      </Routes>
    </BrowserRouter>

  );
}

export default App;
