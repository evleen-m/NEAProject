using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UnityEngine;
public class CyclicQueue
{
    private int[] queue;
    private int maxSize;
    private int head;
    private int tail;


    public CyclicQueue(int maxSize)
    {
        this.maxSize = maxSize;
        this.queue = new int[maxSize];
        this.head = -1; // head will point to first element stored in queue
        this.tail = -1; // tail will point to last element stored in queue
    }

    public void enqueue(int item)
    {
        if (isFull()==true)
        {
            // queue is full so nothing has been added
        }
        else if (head == -1) //first element to be added in the queue
        {
            head = 0; // must set both to 0 or else head will be -1 and tail 0
            tail = 0; // head will not point to first item but instead below the queue
            queue[head] = item;
        }
        else if ((tail == maxSize - 1) && (head != 0))
        {
            tail = 0;
            queue[tail] = item;
            // checks if tail is at the last item and adds it to queue if there is free space available
        }
        else
        {
            // adds item to queue at tail+1
            tail++;
            queue[tail] = item;
        }
    }

    public int dequeue()
    {
        if (isEmpty()== true)
        {
            return -1; // queue is empty so nothing is returned
        }

        var temp = queue[head];
        queue[head] = -1;
        // temp is stored here to allow the correct value of head to be given

        if (head == tail)
        {
            head = -1;
            tail = -1;
            // item was the first/only element in queue
       
        }
        else if (head == maxSize-1)
        {
            head = 0;
            // item was the last item in the array -> cycles back to front of array
        }
        else
        {
            head++;// item is removed, normal case
        }
        return temp;
    }
    public bool isEmpty()
    {
        // if queue is empty both head and tail are at -1, only one of these needs to be checked
        if (head == -1) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isFull()
    {
        // if 
        if ((head == 0 && tail == maxSize - 1) || (tail == (head - 1) % (maxSize - 1)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int[] randomise()
    {
        System.Random random = new System.Random();
        int length = maxSize - 1;
        
        // loop from the last index to the second element
        for (int i = length - 1; i > 0; i--)
        {
            // Pick a random index before the current index
            int randomIndex = random.Next(0, i); // Random index in [0, i)

            // Swap the elements at indices i and j
            var temp = queue[i];
            queue[i] = queue[randomIndex];
            queue[randomIndex] = temp;
        }
        //queue is now shuffled
        return queue;
    }

    public int getHead()
    {
        return head;
    }
    public int getValueInHead()
    {
        return queue[head];
    }
    public int getMaxSize()
    {
        return maxSize;
    }

    String msg;
    public String getQueue()
    {
        msg = "";
        
        if (tail == -1)
        {
           
        }
        if (tail >= head)
        {
            for(int i = head; i <= tail; i++)
            {
                msg = msg+ queue[i] + " ";
            }
        }
        else
        {
            for (int i = head; i < maxSize; i++)
            {
                msg = msg +queue[i] + " ";
            }
            for (int i = 0; i <= tail; i++)
            {
                msg = msg + queue[i] + " ";
            } 
        }
        return msg;
        
    }

}

