// node_modules
import React, { Component } from "react";
import ButtonToolbar from "react-bootstrap/ButtonToolbar";
import Button from "react-bootstrap/Button";
import Table from "react-bootstrap/Table";
import Spinner from "react-bootstrap/Spinner";
import { getVmStatus, startVm, stopVm } from "../../api/virtualMachine";

export class Home extends Component {
  state = {
    hasLoaded: false,
    isStarting: false,
    isStopping: false,
    vmState: null
  };
  componentDidMount = async () => {
    const vmState = await getVmStatus();
    this.setState({
      hasLoaded: true,
      vmState: vmState
    });
  };
  displayVmStatus = vmState => {
    let state = vmState.statuses[1].code.split("/")[1];
    state = state === 'deallocated' ? "stopped" : state;
    state = state === 'deallocating' ? "stopping" : state;
    return state;
  };
  startVm = async () => {
    this.setState(
      {
        isStarting: true
      },
      async () => {
        const vmState = await startVm();
        this.setState({
            hasLoaded: true,
            isStarting: false,
            vmState: vmState
        })
      }
    );
  };
  stopVm = async () => {
    this.setState(
      {
        isStopping: true
      },
      async () => {
        const vmState = await stopVm();
        this.setState({
            hasLoaded: true,
            isStopping: false,
            vmState: vmState
        })
      }
    );
  };
  operationInProgress = () => {
      const { isStarting, isStopping} = this.state;
      return isStarting || isStopping;
  }
  render() {
    const { hasLoaded, isStarting, isStopping, vmState } = this.state;
    if (!hasLoaded) return "Loading...";
    return (
      <>
        <Table striped bordered hover variant="dark">
          <tbody>
            <tr>
              <td>Server status</td>
              <td>{this.displayVmStatus(vmState)}</td>
            </tr>
          </tbody>
        </Table>
        <ButtonToolbar>
          <Button onClick={this.startVm} variant="success" disabled={this.operationInProgress()}>
            Start { isStarting && <Spinner animation="border" />}
          </Button>
          <Button onClick={this.stopVm} variant="danger" disabled={this.operationInProgress()}>
              Stop { isStopping && <Spinner animation="border" />}
            </Button>
        </ButtonToolbar>
      </>
    );
  }
}
