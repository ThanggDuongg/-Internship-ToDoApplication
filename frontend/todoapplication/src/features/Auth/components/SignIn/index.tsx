import { Alert, Button, Snackbar, TextField } from '@mui/material';
import { LoadingButton } from '@mui/lab';
import { yupResolver } from '@hookform/resolvers/yup';
import { Controller, FormProvider, useForm } from 'react-hook-form';
import * as yup from 'yup';
import userApi from '../../../../api/userApi';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import jwt from 'jwt-decode';

const signinSchema = yup.object().shape({
  username: yup.string().required('⚠ Username invalid'),
  password: yup.string().required('⚠ Password invalid'),
});

const btnstyle = { margin: '8px 0' };
function SignIn(props: any) {
  const navigate = useNavigate();
  const [Username, setUsername] = useState();
  const [Password, setPassword] = useState();
  const [Loading, setLoading] = useState(false);
  const [alert, setAlert] = useState(false);
  const [message, setMessage] = useState('');

  const _onHandleSubmit = async (e: any) => {
    const params = {
      username: Username,
      password: Password,
    };
    setLoading(true);
    try {
      const response = await userApi.login(params);
      if (response.data.success) {
        localStorage.setItem('auth', JSON.stringify(response.data.data));
        const user = jwt(response.data.data.accessToken);
        localStorage.setItem('user', JSON.stringify(user));
        navigate('/todolist');
      }
    } catch (err: any) {
      if (err.response.data.message !== undefined) {
        setMessage(err.response.data.message);
      } else {
        setMessage(err.message);
      }
      setTimeout(() => {
        setAlert(true);
      }, 1000);
    } finally {
      setLoading(false);
    }
  };

  const changeUsername = (e: any) => {
    const { value, name } = e.target;
    setUsername(value);
  };

  const changePassword = (e: any) => {
    const { value, name } = e.target;
    setPassword(value);
  };

  return (
    <form>
      <Snackbar
        open={alert}
        autoHideDuration={3000}
        onClose={() => {
          setAlert(false);
        }}
      >
        <Alert
          onClose={() => {
            setAlert(false);
          }}
          variant="filled"
          severity="error"
          sx={{ width: '100%' }}
        >
          {message}
        </Alert>
      </Snackbar>
      <TextField
        label="Username"
        placeholder="Enter username"
        fullWidth
        value={Username || ''}
        sx={{
          marginBottom: '10px',
        }}
        onChange={changeUsername}
      />
      <TextField
        label="Password"
        placeholder="Enter password"
        type="password"
        value={Password || ''}
        fullWidth
        required
        onChange={changePassword}
      />
      <LoadingButton
        loading={Loading}
        color="primary"
        variant="contained"
        style={btnstyle}
        fullWidth
        onClick={_onHandleSubmit}
      >
        Submit
      </LoadingButton>
    </form>
  );
}

export default SignIn;
