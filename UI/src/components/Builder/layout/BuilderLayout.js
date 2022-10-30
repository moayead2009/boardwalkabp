import { AppBar, Button, Container, Toolbar, Typography } from '@mui/material'
import React from 'react'
import { Outlet, useNavigate } from 'react-router'
import useStateContext from "../../../hooks/useStateContext";

export default function Layout() {
  const { resetContext } = useStateContext()
  const navigate = useNavigate()

    const logout = () => {
        resetContext()
        navigate('/')
        // localStorage.clear();
    }

    return (
        <div>
            <AppBar position="static">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                    <Button color="inherit" onClick={() => navigate('/builder/home')}>Home</Button>
                    </Typography>
                    
                    <Button color="inherit" onClick={() => navigate('../builder/applications')}>Applications</Button>
                    <Button color="inherit" onClick={() => navigate('../builder/categories')}>Categories</Button>
                    <Button color="inherit" onClick={() => navigate('../builder/clients')}>Clients</Button>
                    <Button color="inherit" onClick={() => navigate('../builder/questions')}>Questions</Button>
                    <Button color="inherit" onClick={logout}>Logout</Button>
                </Toolbar>
            </AppBar>
            <Container>
                <Outlet />
            </Container>
        </div>
    )
}