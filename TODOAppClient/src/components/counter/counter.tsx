import React from "react";
import "./counter.css"

interface Props{
    increase: () => void,
    decrease: () => void,
    value: number
}

export const Counter: React.FC<Props> = ({increase, decrease, value}) => {

    return (
        <div className="counter">
            <div className="value">{value}</div>
            <div className="buttons">
                <button className="plus" onClick={increase}>{"<"}</button>
                <button className="minus" onClick={decrease}>{"<"}</button>
            </div>
        </div>
    )
}