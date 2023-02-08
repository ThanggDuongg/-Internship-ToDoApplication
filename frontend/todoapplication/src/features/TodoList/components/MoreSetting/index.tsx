import { ToggleButton, ToggleButtonGroup } from '@mui/material';
import SortByAlphaIcon from '@mui/icons-material/SortByAlpha';
import KeyboardDoubleArrowUpIcon from '@mui/icons-material/KeyboardDoubleArrowUp';
import KeyboardDoubleArrowDownIcon from '@mui/icons-material/KeyboardDoubleArrowDown';
import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';
import EventIcon from '@mui/icons-material/Event';

import { useState } from 'react';

function MoreSetting(props: any) {
  const { orderBy, handleOrderby, sortBy, handleSortby } = props;

  return (
    <>
      <ToggleButtonGroup
        value={sortBy}
        onChange={handleSortby}
        exclusive
        aria-label="text alignment"
      >
        <ToggleButton value="ALPHABETICALLY" aria-label="left aligned">
          <SortByAlphaIcon />
        </ToggleButton>
        <ToggleButton value="DATE_ADDED" aria-label="centered">
          <CalendarMonthIcon />
        </ToggleButton>
        <ToggleButton value="DUE_DATE" aria-label="right aligned">
          <EventIcon />
        </ToggleButton>
      </ToggleButtonGroup>
      <ToggleButtonGroup
        value={orderBy}
        onChange={handleOrderby}
        aria-label="text formatting"
        exclusive
      >
        <ToggleButton value="ASCENDING" aria-label="bold">
          <KeyboardDoubleArrowUpIcon />
        </ToggleButton>
        <ToggleButton value="DESCENDING" aria-label="italic">
          <KeyboardDoubleArrowDownIcon />
        </ToggleButton>
      </ToggleButtonGroup>
    </>
  );
}

export default MoreSetting;
