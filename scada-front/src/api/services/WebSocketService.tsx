import { WEBSOCKET } from "..";


// const [socket, setSocket] = useState<WebSocket | null>(null);
// useEffect(() => {
//   WebSocketService.createSocket(setSocket);
// }, []);
// useEffect(() => {
//   WebSocketService.defineSocket(socket, "NewTagRecordCreated");
// }, [socket]);


export const WebSocketService = {
    createSocket: function(setSocket : any) {
  
      // Create a new WebSocket instance
      const newSocket = new WebSocket(WEBSOCKET());
  
      // Store the WebSocket instance in state
      setSocket(newSocket);
  
      // Cleanup function to close the WebSocket on component unmount
      return () => {
        newSocket.close();
      };
    },

    defineSocket: function(socket : WebSocket | null, topic : string) {
        if (socket) {
            // WebSocket event listener for when the connection is opened
            socket.onopen = () => {
              console.log('Connected to WebSocket.');
              socket.send("subscribe:" + topic)
              //socket.send("subscribe:NewAlarmRecordsCreated")
            };
      
            // WebSocket event listener for when a message is received
            socket.onmessage = async (event) => {
              let message = await event.data.text();
              console.log('Received message:', message);
              // Process the received message
            };
      
            // WebSocket event listener for when the connection is closed
            socket.onclose = (event) => {
              console.log('WebSocket connection closed:', event);
              // Handle WebSocket closure event, you can initiate reconnection logic here
            };
      
            // WebSocket event listener for errors
            socket.onerror = (error) => {
              console.error('WebSocket error:', error);
            };
        }
    }
}