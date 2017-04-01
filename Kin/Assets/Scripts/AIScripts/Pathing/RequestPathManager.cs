using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RequestPathManager : MonoBehaviour {
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentRequest;

    static RequestPathManager instance;
    Pathing pathfinding;

    bool isProcessingPath;

    private void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathing>();
    }

    public static void Request(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[],bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
        Debug.Log("Request Recieved");

    }

    void TryProcessNext()
    {
        if(!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentRequest.pathStart, currentRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> callback)
        {
            pathStart = start;
            pathEnd = end;
            this.callback = callback;
        }
    }
}
