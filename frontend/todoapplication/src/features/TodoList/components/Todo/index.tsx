import {
  Box,
  Button,
  Checkbox,
  Container,
  TextField,
  Typography,
} from '@mui/material';
import { pink } from '@mui/material/colors';
import FeedbackOutlinedIcon from '@mui/icons-material/FeedbackOutlined';
import RemoveCircleOutlineIcon from '@mui/icons-material/RemoveCircleOutline';
import { useEffect, useState } from 'react';
import TaskStatus from '../../../../constants/TaskStatus';

const days = [
  'Sunday',
  'Monday',
  'Tuesday',
  'Wednesday',
  'Thursday',
  'Friday',
  'Saturday',
];
const months = [
  'January',
  'February',
  'March',
  'April',
  'May',
  'June',
  'July',
  'August',
  'September',
  'October',
  'November',
  'December',
];

function Todo(props: any) {
  const { item, deleteTodoFunc, updateTodoFunc } = props;
  const { createdDate, userEntity, ...itemUpdate } = item;
  const [editName, setEditName] = useState(false);
  const [editDescription, setEditDescription] = useState(false);
  const [itemName, setItemName] = useState('');
  const [itemDescription, setItemDescription] = useState('');

  useEffect(() => {
    if (item && item.name && item.name !== itemName) {
      setItemName(item.name);
    }
  }, [editName]);

  useEffect(() => {
    if (item) {
      if (item.description) {
        if (item.description !== itemDescription) {
          console.log('> ', item.description);
          setItemDescription(item.description);
        }
      } else {
        setItemDescription('');
      }
    }
  }, [editDescription]);

  const changeName = (e: any) => {
    setItemName(e.target.value);
  };

  const changeDescription = (e: any) => {
    setItemDescription(e.target.value);
  };

  const handleDueDate = (dueDate: any) => {
    const due_date = new Date(dueDate);
    const date_now = new Date();

    if (due_date < date_now) {
      return `Overdue: ${days[due_date.getDay()]} ${
        months[due_date.getMonth()]
      } ${due_date.getFullYear()}`;
    } else {
      return `Deadline: ${days[due_date.getDay()]} ${
        months[due_date.getMonth()]
      } ${due_date.getFullYear()}`;
    }
  };

  const updateStatusTodo = (e: any) => {
    const temp = item.status === TaskStatus.COMPLETED ? true : false;
    itemUpdate.status = !temp ? TaskStatus.COMPLETED : TaskStatus.UNCOMPLETED;
    updateTodoFunc(itemUpdate);
  };

  const updateNameTodo = (e: any) => {
    if (e.key === 'Enter' && e.ctrlKey) {
      itemUpdate.name = itemName;
      updateTodoFunc(itemUpdate);
      setEditName(false);
    }
  };

  const updateDescriptionTodo = (e: any) => {
    if (e.key === 'Enter' && e.ctrlKey) {
      itemUpdate.description = itemDescription;
      updateTodoFunc(itemUpdate);
      setEditDescription(false);
    }
  };

  return (
    <Container
      maxWidth="md"
      sx={{
        marginTop: '15px',
        marginBottom: '27px',
        display: 'flex',
        border: '0.5px solid #bdc3c7',
        paddingTop: '6px',
        paddingBottom: '6px',
        paddingLeft: '3px',
        paddingRight: '3px',
        borderRadius: '6px',
      }}
    >
      <Box
        sx={{
          display: 'flex',
          marginBottom: '6px',
        }}
      >
        <Checkbox
          checked={item.status === TaskStatus.COMPLETED ? true : false}
          sx={{
            color: pink[800],
            '&.Mui-checked': {
              color: pink[600],
            },
            marginRight: '15px',
            ':hover': { backgroundColor: 'white' },
          }}
          onChange={updateStatusTodo}
        />
        <Box
          sx={{
            width: '730px',
          }}
          margin={item.dueDate ? '' : 'auto'}
        >
          {editName ? (
            <TextField
              id="TodoName"
              sx={{
                width: '720px',
                marginBottom: '5px',
              }}
              color="secondary"
              variant="standard"
              multiline
              focused
              value={itemName || ''}
              onBlur={() => setEditName(false)}
              onChange={changeName}
              onKeyDown={updateNameTodo}
            />
          ) : (
            <Typography
              variant="body2"
              gutterBottom
              sx={{
                cursor: 'pointer',
                marginBottom: '0px',
              }}
              onClick={() => setEditName(true)}
            >
              {item.name}
            </Typography>
          )}
          {editDescription ? (
            <TextField
              sx={{
                width: '720px',
                marginBottom: '5px',
              }}
              color="primary"
              value={itemDescription || ''}
              variant="standard"
              focused
              onBlur={() => setEditDescription(false)}
              onChange={changeDescription}
              onKeyDown={updateDescriptionTodo}
            />
          ) : (
            <Typography
              variant="caption"
              gutterBottom
              sx={{
                marginBottom: '0px',
                marginTop: '20px',
                cursor: 'pointer',
              }}
              onClick={() => setEditDescription(true)}
            >
              {item.description ? item.description : 'No description'}
            </Typography>
          )}
          {item.dueDate && (
            <Box sx={{ display: 'flex', marginTop: '10px' }}>
              <FeedbackOutlinedIcon
                sx={{ color: '#ff7979', marginRight: '10px', fontSize: '19px' }}
              />
              <Typography variant="caption" display="block" gutterBottom>
                {handleDueDate(item.dueDate)}
              </Typography>
            </Box>
          )}
        </Box>
      </Box>
      <Box
        sx={{
          margin: 'auto 0',
        }}
      >
        <Button
          sx={{
            color: '#ff7979',
            ':hover': {
              backgroundColor: 'transparent',
            },
          }}
          onClick={() => deleteTodoFunc(item.id)}
        >
          <RemoveCircleOutlineIcon />
        </Button>
      </Box>
    </Container>
  );
}

export default Todo;
