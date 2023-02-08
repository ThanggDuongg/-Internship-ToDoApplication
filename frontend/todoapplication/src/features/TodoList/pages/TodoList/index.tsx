import { Abc } from '@mui/icons-material';
import {
  Alert,
  AlertColor,
  Box,
  Button,
  CircularProgress,
  Container,
  Pagination,
  Snackbar,
  Typography,
} from '@mui/material';
import { useEffect, useState } from 'react';
import taskApi from '../../../../api/taskApi';
import TaskStatus from '../../../../constants/TaskStatus';
import CreateTodo from '../../components/CreateTodo';
import Todo from '../../components/Todo';
import ViewHeader from '../../components/ViewHeader';

function TodoList(props: any) {
  const user = JSON.parse(localStorage.getItem('user') || '{}');
  const userId = user.userid;
  const [taskList, setTaskList] = useState([]);
  const [currentPage, setCurrrentPage] = useState(1);
  const [totalPage, setTotalPage] = useState(1);
  const [orderBy, setOrderBy] = useState<string | null>('ASCENDING');
  const [sortBy, setSortBy] = useState<string | null>('DATE_ADDED');
  const [statusTask, setStatusTask] = useState(false); // ~uncompleted
  const [createLoading, setCreateLoading] = useState(false);
  const [message, setMessage] = useState('');
  const [alert, setAlert] = useState(false);
  const [statusAlert, setStatusAlert] = useState<AlertColor | undefined>(
    'success',
  );
  const [isLoading, setIsLoading] = useState(false);

  const handleOrderby = (
    event: React.MouseEvent<HTMLElement>,
    orderBy: string | null,
  ) => {
    setOrderBy(orderBy);
    fetchTask(userId);
  };

  const handleSortby = (
    event: React.MouseEvent<HTMLElement>,
    sortBy: string | null,
  ) => {
    setSortBy(sortBy);
    fetchTask(userId);
  };

  const handleChange = (e: any, p: any) => {
    setCurrrentPage(p);
  };

  const fetchTask = async (userId: any) => {
    setIsLoading(true);
    try {
      const params = {
        taskStatus: !statusTask ? TaskStatus.UNCOMPLETED : TaskStatus.COMPLETED,
        UserId: userId,
        Page: currentPage,
        ItemsPerPage: 3,
        sortBy: sortBy,
        orderBy: orderBy,
      };
      const response = await taskApi.getAllByUserId(params);
      setTaskList(response.data.data.items);
      setCurrrentPage(response.data.data.pageInfo.currentPage);
      setTotalPage(response.data.data.pageInfo.totalPages);
    } catch (err) {
      console.log('Failed to fetch tasks data: ', err);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    if (totalPage !== 0) {
      fetchTask(userId);
    }
    console.log(taskList);
  }, [currentPage, sortBy, orderBy, statusTask]);

  const deleteTodo = async (id: any) => {
    const params = {
      id: id,
    };
    try {
      const response = await taskApi.delete(params);
      if (response.status === 204) {
        setStatusAlert('success');
        setMessage('Delete todo successfully');
        setTimeout(() => {
          setAlert(true);
        }, 1000);
        fetchTask(userId);
      }
    } catch (err: any) {
      setStatusAlert('error');
      if (err.response.data.message !== undefined) {
        setMessage(err.response.data.message);
      } else {
        setMessage(err.message);
      }
      setTimeout(() => {
        setAlert(true);
      }, 1000);
    }
  };

  const updateTodo = async (params: any) => {
    try {
      const response = await taskApi.update(params);
      if (response.data.success) {
        setStatusAlert('success');
        setMessage(response.data.message);
        setTimeout(() => {
          setAlert(true);
        }, 1000);
        fetchTask(userId);
      }
    } catch (err: any) {
      setStatusAlert('error');
      if (err.response.data.message !== undefined) {
        setMessage(err.response.data.message);
      } else {
        setMessage(err.message);
      }
      setTimeout(() => {
        setAlert(true);
      }, 1000);
    }
  };

  const createNewTodo = async (params: any) => {
    setCreateLoading(true);
    try {
      const response = await taskApi.create(params);
      if (response.data.success) {
        setStatusTask(false);
        fetchTask(userId);
        setStatusAlert('success');
        setMessage(response.data.message);
        setTimeout(() => {
          setAlert(true);
        }, 1000);
      }
    } catch (err: any) {
      setStatusAlert('error');
      if (err.response.data.message !== undefined) {
        setMessage(err.response.data.message);
      } else {
        setMessage(err.message);
      }
      setTimeout(() => {
        setAlert(true);
      }, 1000);
    } finally {
      setCreateLoading(false);
    }
  };

  const changeStatusTask = () => {
    setStatusTask(!statusTask);
    fetchTask(userId);
  };

  return (
    <Box
      style={{
        paddingLeft: '200px',
        paddingRight: '200px',
      }}
    >
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
          severity={statusAlert}
          sx={{ width: '100%' }}
        >
          {message}
        </Alert>
      </Snackbar>
      <ViewHeader
        orderBy={orderBy}
        handleOrderby={handleOrderby}
        sortBy={sortBy}
        handleSortby={handleSortby}
        statusTask={statusTask}
        setStatusTaskFunc={changeStatusTask}
      />
      {/* {!isLoading ? (
        taskList &&
        taskList.map((item: any, index) => (
          <Todo
            key={index}
            item={item}
            deleteTodoFunc={deleteTodo}
            updateTodoFunc={updateTodo}
          />
        ))
      ) : (
        <Container
          maxWidth="md"
          sx={{
            marginTop: '15px',
            marginBottom: '27px',
            display: 'flex',
            paddingTop: '6px',
            paddingBottom: '6px',
            paddingLeft: '3px',
            paddingRight: '3px',
            borderRadius: '6px',
          }}
        >
          <CircularProgress sx={{ margin: 'auto' }} />
        </Container>
      )} */}
      {taskList &&
        taskList.map((item: any, index) => (
          <Todo
            key={index}
            item={item}
            deleteTodoFunc={deleteTodo}
            updateTodoFunc={updateTodo}
          />
        ))}
      <Container maxWidth="sm" style={{ display: 'flex' }}>
        {totalPage !== 0 ? (
          <Pagination
            count={totalPage}
            color="secondary"
            style={{ margin: 'auto' }}
            onChange={handleChange}
          />
        ) : (
          <Typography
            style={{
              margin: 'auto',
            }}
            variant="h6"
            gutterBottom
          >
            Empty
          </Typography>
        )}
      </Container>
      <CreateTodo createTodoFunc={createNewTodo} loading={createLoading} />
    </Box>
  );
}

export default TodoList;
