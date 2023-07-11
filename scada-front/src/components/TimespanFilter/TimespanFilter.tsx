import React, { useState } from 'react'
import { Form, Button } from 'react-bootstrap'
import { formatDate } from '../../utils/DateFormatter'

const TimespanFilter = (props: { handleSubmit: any }) => {
    const [dateFrom, setDateFrom] = useState<Date>(new Date())
    const [dateTo, setDateTo] = useState<Date>(new Date());

    const handleDateFromChange = (date: Date) => {
        setDateFrom(date);
    };

    const handleDateToChange = (date: Date) => {
        setDateTo(date);
    };

    return (
        <div className="row align-items-center margin-bottom">
            <div className="col-md-5">
                <Form.Group controlId="dateFrom" className="d-flex align-items-center vertical-align justify-content-between">
                    <Form.Label className="mr-2">From:</Form.Label>
                    <Form.Control type="date" name="dateFromDate" onChange={(e) => handleDateFromChange(new Date(e.target.value))} />
                </Form.Group>
            </div>

            <div className="col-md-5">
                <Form.Group controlId="dateTo" className="d-flex align-items-center vertical-align justify-content-between">
                    <Form.Label className="mr-2">To:</Form.Label>
                    <Form.Control type="date" name="dateToDate" onChange={(e) => handleDateToChange(new Date(e.target.value))} />
                </Form.Group>
            </div>

            <div className='col-md-1'></div>

            <div className="col-md-1">
                <Button onClick={() => props.handleSubmit(dateFrom, dateTo)} variant="primary">
                    Filter
                </Button>
            </div>
        </div>
    )
}

export default TimespanFilter