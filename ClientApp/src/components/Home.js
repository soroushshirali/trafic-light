import React, { useState, useEffect } from 'react';
import TrafficLight from './TrafficLight';
import { HubConnectionBuilder } from '@aspnet/signalr';
import './Home.css';

export function Home() {
    const [connection, setConnection] = useState(null);
    const [lights, setLights] = useState({ north: 'green', south: 'green', east: 'red', west: 'red' });

    //connecting to hub when this component mounts only
    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('hubs/traffic')
            .build();

        setConnection(newConnection);
    }, []);

    useEffect(() => {
        if (connection) {
            connection.start()
                .then(result => {
                    console.log('Connected!');

                    connection.on('ReceiveNewStatus', status => {
                        console.log(status);

                        setLights(status);
                    });
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [connection]);

    return (
        <div className="grid-container">
            <div className="grid-item"><span>N</span><TrafficLight light={lights.north} /></div>
            <div className="grid-item"><span>E</span><TrafficLight light={lights.east} /></div>
            <div className="grid-item"><span>W</span><TrafficLight light={lights.west} /></div>
            <div className="grid-item"><span>S</span><TrafficLight light={lights.south} /></div>
        </div>
    );
}
