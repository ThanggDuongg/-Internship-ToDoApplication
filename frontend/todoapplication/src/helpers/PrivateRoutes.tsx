import { Navigate, Outlet, useLocation } from 'react-router-dom';

function PrivateRoutes(props: any) {
  const location = useLocation();
  const user = localStorage.getItem('user') || null;
  if (user === null) {
    return <Navigate to="/signin" state={{ from: location }} replace />;
  } else {
    return <Outlet />;
  }
}

export default PrivateRoutes;
