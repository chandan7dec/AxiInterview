import React, { useEffect, useState } from "react";
import "./App.css";

const MyApp = () => {
    //get the locale info from the browser
    const userLocale = navigator.language;
    // Declare React states
    const [buttonColor, setButtonColor] = useState("red");
    const [lastClicked, setLastClicked] = useState(new Date().toLocaleString(userLocale));
    const [width, setWidth] = useState(window.innerWidth);

    useEffect(() => {
        // Function to update the state with the new screen width
        const handleResize = () => setWidth(window.innerWidth);

        // Add event listener to handle window resize
        window.addEventListener('resize', handleResize);

        // Cleanup function to remove event listener when component unmounts
        return () => window.removeEventListener('resize', handleResize);
    }, []);
    const getNextButtonColor = () => {
        switch (buttonColor) {
          case "red":
            return "blue";
          case "blue":
            return "green";
          case "green":
            return "red";
          default:
            throw new Error("Invalid color");
        }
      };
    
      const onClick = () => {
        setButtonColor(getNextButtonColor());
        setLastClicked(new Date().toLocaleString(userLocale));
      };

      
      const convertToGMT = (localTimeString : string) => {
        // Create a Date object from the local time string
        const localDate = new Date(localTimeString);
      
        // Get the UTC string
        return localDate.toISOString();
      }

      const convertToACT = (localTimeString : string) => {
        // Create a Date object from the local time string
        const localDate = new Date(localTimeString);
      
        // Convert the time to ACT (Australia/Adelaide timezone)
        const actTimeString = localDate.toLocaleString('en-AU', {
          timeZone: 'Australia/Adelaide',
          year: 'numeric',
          month: '2-digit',
          day: '2-digit',
          hour: '2-digit',
          minute: '2-digit',
          second: '2-digit',
          hour12: false, // Use 24-hour format
        });
      
        return actTimeString;
      };

    // Conditional class name
    const TimeContainerClass = `TimeContainer ${width < 600 ? 'TimeContainer-Direction' : ''}`;
    // console.log(TimeContainerClass);
    return (
        <>
          <div>
            <button
              onClick={onClick}
              style={{ backgroundColor: buttonColor }}
            >
              Click Here to get Latest Time
            </button>
          </div>
          <div className={TimeContainerClass}>
            <div className="TimeItem">
              Local time:{" "}
              {lastClicked !== undefined ? lastClicked.toString() : "Never"}
            </div>
            <div className="TimeItem">
              GMT Time:{" "}
              {lastClicked !== undefined ? convertToGMT(lastClicked.toString()) : "Never"}
            </div>
            <div className="TimeItem">
              ACT Time:{" "}
              {lastClicked !== undefined ? convertToACT(lastClicked.toString()) : "Never"}
            </div>
          </div>
        </>
      );
}

export default MyApp
