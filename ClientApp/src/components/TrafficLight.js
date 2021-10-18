import React, { Component } from 'react';
import './TrafficLight.css';

//todo: add test for this one
//todo: add typescript and define enum for colors

const TrafficLight = (props) => {
    return <div className="light-container">
        <div className={"circle " + (props.light == 'red' ? 'red' : '')} color="red"></div>
        <div className={"circle " + (props.light == 'yellow' ? 'yellow' : '')} color="yellow"></div>
        <div className={"circle " + (props.light == 'green' ? 'green' : '')} color="green"></div>
    </div>;
};
export default TrafficLight;