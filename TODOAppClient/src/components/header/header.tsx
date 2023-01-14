import React from 'react';
import logo from './../../logo.svg';
import "./header.css"

export const Header: React.FC = () => {
    return (
        <div className="header">
            <span data-testid="name">Barclays TODO Assesment</span>
            <img src={logo} className="Header-logo" alt="logo" />
        </div>
    )
}