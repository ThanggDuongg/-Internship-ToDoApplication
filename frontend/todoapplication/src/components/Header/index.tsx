import { Typography, AppBar, Toolbar, Button, Tooltip } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const style01 = { display: 'flex', flexDirection: 'column' };
const style02 = {};
function Header(props: any) {
  const user = JSON.parse(localStorage.getItem('user') || '{}');
  const navigate = useNavigate();

  const logout = () => {
    localStorage.removeItem('user');
    localStorage.removeItem('auth');
    navigate('/signin');
  };

  return (
    <AppBar
      style={{
        alignItems: 'center',
        backgroundColor: '#ff7979',
      }}
    >
      <Toolbar sx={Object.keys(user).length !== 0 ? style01 : style02}>
        <Typography
          variant="h6"
          component="div"
          style={{
            fontWeight: 'bold',
            textTransform: 'uppercase',
            textShadow: '#34495e 1px 2px',
            color: '#f1f2f6',
          }}
        >
          Todo Application
        </Typography>
        {user && Object.keys(user).length !== 0 && (
          <Tooltip title="logout">
            <Button
              sx={{
                ':hover': {
                  background: 'none',
                },
              }}
              onClick={logout}
            >
              Hello: {user.name}
            </Button>
          </Tooltip>
        )}
      </Toolbar>
    </AppBar>
  );
}

export default Header;
