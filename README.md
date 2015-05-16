
# ExtendedCollectionsSharp
Some common and not that common collections implemented in .net. 

## CircularBuffer
Basically, it's a queue with fixed size.
###Main features
1. FIFO!
2. When overflowed, the buffer drops the first element and queues the new one. for example: 
  Circular buffer with size 4 elements.</br>
  <code>3 4 5 6</code> </br>
  The user passes the element 7, so the buffer now looks like this:</br>
  <code>4 5 6 7</code>
