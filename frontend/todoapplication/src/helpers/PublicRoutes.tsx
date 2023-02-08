import { Navigate, Outlet, useLocation } from 'react-router-dom';

function PublicRoutes(props: any) {
  const location = useLocation();
  const user = localStorage.getItem('user');

  if (user) {
    return <Navigate to="/signin" state={{ from: location }} replace />;
  } else {
    return <Outlet />;
  }
}

export default PublicRoutes;
