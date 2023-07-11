import React from 'react';
import { Navbar, Container, Nav } from 'react-bootstrap';
import style  from './Menu.module.css'; 

const Menu: React.FC = () => {
    return (
        <Navbar bg="light" expand="lg">
            <Container>
                <Navbar.Brand>
                    <img
                        src="favicon.ico"
                        width="30"
                        height="30"
                        className="d-inline-block align-top"
                        alt="App Logo"
                    />{' '}
                    Scada
                </Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav" className="justify-content-end">
                    <Nav>
                        <Nav.Link href="#" className={style.menuLink}>Database Manager</Nav.Link>
                        <Nav.Link href="#" className={style.menuLink}>Trending</Nav.Link>
                        <Nav.Link href="#" className={style.menuLink}>Alarm Display</Nav.Link>
                        <Nav.Link href="#" className={style.menuLink}>Report Manager</Nav.Link>
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
};

export default Menu;
