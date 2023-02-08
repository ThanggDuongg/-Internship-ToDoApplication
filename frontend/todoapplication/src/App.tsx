import Header from './components/Header';
import Main from './features/Auth/pages/Main';
import TodoList from './features/TodoList/pages/TodoList';
import {
  BrowserRouter as Router,
  Navigate,
  Route,
  Routes,
  useNavigate,
} from 'react-router-dom';
import React from 'react';
import PrivateRoutes from './helpers/PrivateRoutes';
import PublicRoutes from './helpers/PublicRoutes';

function App() {
  return (
    <>
      <Router>
        <Header />
        {/* <TodoList /> */}
        <Routes>
          {/* <Route path='*' element={<NotFound />} /> */}
          {/* <Route element={<PublicRoutes />}> */}
          <Route path="/" element={<Navigate to="/signup" replace />} />
          <Route
            path="/signup"
            element={
              <React.Suspense fallback={<>Loading...</>}>
                <Main />
              </React.Suspense>
            }
          />

          <Route
            path="/signin"
            element={
              <React.Suspense fallback={<>Loading...</>}>
                <Main />
              </React.Suspense>
            }
          />
          {/* </Route> */}

          <Route element={<PrivateRoutes />}>
            <Route
              path="/todolist"
              element={
                <React.Suspense fallback={<>Loading...</>}>
                  <TodoList />
                </React.Suspense>
              }
            />
          </Route>
        </Routes>
      </Router>
    </>
  );
}

export default App;
