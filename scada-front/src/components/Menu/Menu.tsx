import React, { useEffect, useState } from 'react';
import { Navbar, Container, Nav, Button } from 'react-bootstrap';
import style from './Menu.module.css';
import { Link } from 'react-router-dom';

const Menu: React.FC = () => {

    const [session, setSession] = useState<boolean>();
    const logout = () => {
        localStorage.removeItem("token")
        window.location.href = ""
        setSession(false)
    }

    useEffect(() => {
        setSession(localStorage.getItem("token") !== null);
    }, [])

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
                        <Nav.Link href="/database-manager" className={style.menuLink}>Database Manager</Nav.Link>
                        <Nav.Link href="/trending" className={style.menuLink}>Trending</Nav.Link>
                        <Nav.Link href="/alarm-display" className={style.menuLink}>Alarm Display</Nav.Link>
                        <Nav.Link href="/report-manager" className={style.menuLink}>Report Manager</Nav.Link>
                        {session &&
                            <Button variant="white" className={style.logout} onClick={() => logout()}>
                                <i className="bi bi-box-arrow-right"></i>
                            </Button>
                        }
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
};

export default Menu;
