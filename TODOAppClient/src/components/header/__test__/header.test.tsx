import React from "react";
import { render, cleanup, screen } from "@testing-library/react";
import "@testing-library/jest-dom/extend-expect";
import renderer from "react-test-renderer";

import { Header } from "../header";

afterEach(cleanup);
it("header renders without crashing", () => {
    render(<Header/>);
})

it("header includes Barclays TODO Assesment", () => {
    render(<Header/>);
    const name = screen.getByTestId("name");
    expect(name).toHaveTextContent("Barclays TODO Assesment");
})

it("matches snapshot", () => {
    const tree = renderer.create(<Header/>).toJSON();
    expect(tree).toMatchSnapshot();
})