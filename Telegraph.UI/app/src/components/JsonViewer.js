import React, { Component } from 'react';
import ReactJson from 'react-json-view';

export class JsonViewer extends Component {
    get displayName() {
        return 'JsonViewer';
    }

    constructor(props) {
        super(props);

        this.state = { json: [] };

        fetch('api/json')
            .then(response => response.json())
            .then(data => {
                this.setState({ json: data });
            });
    }

    render() {
        let response = this.state.json;

        return (
            <div>
                <h1>JSON Viewer</h1>

                <p>This is a simple example of a React component.</p>

                <ReactJson src={response} />
            </div>
        );
    }
}
