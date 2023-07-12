import React, { useState } from 'react';
import { Form, Button, Alert } from 'react-bootstrap';
import style from './LoginComponent.module.css';
import { AuthService } from '../../services/AuthService';

const LoginComponent: React.FC = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        try {
            const jwt = await AuthService.login({ Username: username, Password: password });
            console.log(jwt);
            localStorage.setItem("token", jwt)
            window.location.href = "/trending"

            setErrorMessage('');
            console.log('Logged in successfully!');
        }
        catch (error: any) {
            setErrorMessage(error.response.data);
        }
    };

    return (
        <div className={style.content}>
            <h2>Login</h2>
            {errorMessage && <Alert variant="danger">{errorMessage}</Alert>}
            <Form onSubmit={handleSubmit}>
                <Form.Group controlId="formUsername">
                    <Form.Label style={{ marginTop: '10px' }}>Username</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder="Enter username"
                        value={username}
                        onChange={(event) => setUsername(event.target.value)}
                    />
                </Form.Group>
                <Form.Group controlId="formPassword">
                    <Form.Label style={{ marginTop: '10px' }}>Password</Form.Label>
                    <Form.Control
                        type="password"
                        placeholder="Enter password"
                        value={password}
                        onChange={(event) => setPassword(event.target.value)}
                    />
                </Form.Group>
                <Button variant="primary" type="submit" style={{ marginTop: '10px' }}>
                    Log in
                </Button>
            </Form>
        </div>
    );
};

export default LoginComponent;
