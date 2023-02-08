import { Button, Container, TextField } from '@mui/material';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import React, { useState } from 'react';
import taskApi from '../../../../api/taskApi';
import { LoadingButton } from '@mui/lab';

const btnstyle = { margin: '8px 0' };
function CreateTodo(props: any) {
  const user = JSON.parse(localStorage.getItem('user') || '{}');
  const userId = user.userid;
  const { createTodoFunc, loading } = props;
  const [value, setValue] = React.useState<Date | null>(null);
  const [nameTodo, setNameTodo] = React.useState<string | null | undefined>();
  const [descriptionTodo, setDescriptionTodo] = React.useState<
    string | null | undefined
  >();
  const [statusComponent, setStatusComponent] = useState(false);

  const createNewTodo = async () => {
    const currentDate = new Date();
    const params = {
      name: nameTodo,
      description: descriptionTodo,
      createdDate: currentDate.toJSON(),
      dueDate: value?.toJSON(),
      userId: userId,
    };
    createTodoFunc(params);
    setNameTodo(null);
    setDescriptionTodo(null);
    setValue(null);
    setStatusComponent(false);
  };

  const changeNameTodo = (e: any) => {
    setNameTodo(e.target.value);
  };

  const changeDescriptionTodo = (e: any) => {
    setDescriptionTodo(e.target.value);
  };

  return (
    <Container maxWidth="sm">
      {!statusComponent ? (
        <Button
          type="button"
          color="primary"
          variant="contained"
          style={btnstyle}
          onClick={() => setStatusComponent(true)}
          fullWidth
        >
          Create new
        </Button>
      ) : (
        <form style={{ marginTop: '15px' }}>
          <TextField
            label="Name"
            placeholder="Enter Name Todo"
            fullWidth
            value={nameTodo || ''}
            onChange={changeNameTodo}
            sx={{
              marginBottom: '10px',
            }}
          />
          <TextField
            label="Description"
            placeholder="Enter Description Todo"
            fullWidth
            value={descriptionTodo || ''}
            onChange={changeDescriptionTodo}
            sx={{
              marginBottom: '10px',
            }}
            required
          />
          <LocalizationProvider dateAdapter={AdapterDateFns}>
            <DatePicker
              label="Due date"
              value={value}
              disablePast
              onChange={(newValue) => {
                setValue(newValue);
              }}
              renderInput={(params) => <TextField fullWidth {...params} />}
            />
          </LocalizationProvider>
          <LoadingButton
            loading={loading}
            type="button"
            color="primary"
            variant="contained"
            style={btnstyle}
            fullWidth
            onClick={createNewTodo}
          >
            Create
          </LoadingButton>
          <Button
            type="button"
            color="secondary"
            variant="contained"
            style={btnstyle}
            fullWidth
            onClick={() => {
              setStatusComponent(false);
            }}
          >
            Cancel
          </Button>
        </form>
      )}
    </Container>
  );
}

export default CreateTodo;
