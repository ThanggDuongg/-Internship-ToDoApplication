import { Box, Typography, Divider, Tooltip, Button } from '@mui/material';
import IconButton from '@mui/material/IconButton';
import { useState } from 'react';
import MoreSetting from '../MoreSetting';

function ViewHeader(props: any) {
  const {
    orderBy,
    handleOrderby,
    sortBy,
    handleSortby,
    statusTask,
    setStatusTaskFunc,
  } = props;
  //const [statusTodo, setStatusTodo] = useState(false); // false = uncompleted

  return (
    <Box
      style={{
        marginTop: '80px',
      }}
    >
      <Box
        sx={{
          display: 'flex',
          flexDirection: 'row',
          flexWrap: 'wrap',
          justifyContent: 'space-around',
          marginBottom: '25px',
        }}
      >
        <Typography
          variant={'h6'}
          style={{
            color: '#2f3640',
            lineHeight: '2.6',
          }}
        >
          Todo List
        </Typography>
        <MoreSetting
          orderBy={orderBy}
          handleOrderby={handleOrderby}
          sortBy={sortBy}
          handleSortby={handleSortby}
        />
      </Box>
      <Divider
        sx={{
          marginBottom: '20px',
        }}
      />
      <Box display={'flex'}>
        <Button
          sx={{ margin: 'auto' }}
          variant="contained"
          color="secondary"
          onClick={setStatusTaskFunc}
        >
          {statusTask === false ? 'Uncompleted' : 'Completed'}
        </Button>
      </Box>
    </Box>
  );
}

export default ViewHeader;
