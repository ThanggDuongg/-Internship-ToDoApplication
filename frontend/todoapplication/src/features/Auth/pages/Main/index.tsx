import {
  Button,
  Checkbox,
  FormControlLabel,
  Grid,
  Link,
  Paper,
  TextField,
  Typography,
} from '@mui/material';
import SignIn from '../../components/SignIn';
import SignUp from '../../components/SignUp';
import { Link as ReactRouterDom_Link, useLocation } from 'react-router-dom';

function Main(props: any) {
  const { pathname } = useLocation();
  const redirect = () => {
    if (pathname === '/signup') {
      return <SignUp />;
    }
    if (pathname === '/signin') {
      return <SignIn />;
    }
  };

  const paperStyle = {
    padding: 20,
    height: '35vh',
    width: 280,
    margin: '20px auto',
  };

  return (
    <Grid
      sx={{
        marginTop: '200px',
      }}
    >
      <Paper elevation={10} style={paperStyle}>
        <Grid alignItems={'center'}>
          <Typography
            variant={'h5'}
            sx={{
              marginBottom: '20px',
            }}
          >
            {pathname === '/signup' ? 'Sign Up' : 'Sign In'}
          </Typography>
        </Grid>
        {redirect()}
        {pathname === '/signup' ? (
          <Typography>
            {' '}
            Do you have an account ?
            <Link component={ReactRouterDom_Link} to="/signin">
              Sign In
            </Link>
          </Typography>
        ) : (
          <Typography>
            {' '}
            New to TodoApplication ?
            <Link component={ReactRouterDom_Link} to="/signup">
              Sign Up
            </Link>
          </Typography>
        )}
      </Paper>
    </Grid>
  );
}

export default Main;
