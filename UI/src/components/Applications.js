import React from 'react'
import useStateContext from '../hooks/useStateContext'

export default function Applications() {
    const {context} = useStateContext()
    console.log(context)
    
  return (
    <div>
    <h1>Applications</h1>
    <p>Hello from applications {this.props.name} {this.props.children}</p>
    </div>
  )
}
