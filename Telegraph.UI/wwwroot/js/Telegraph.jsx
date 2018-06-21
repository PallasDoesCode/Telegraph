class TextField extends React.Component {
  render() {
    return (
      <div className="textField">
        Hello, world! I am a textfield.
      </div>
    );
  }
}

ReactDOM.render(
  <TextField />,
  document.getElementById('content')
);

