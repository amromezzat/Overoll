/*Licensed to the Apache Software Foundation (ASF) under one
or more contributor license agreements.  See the NOTICE file
distributed with this work for additional information
regarding copyright ownership.  The ASF licenses this file
to you under the Apache License, Version 2.0 (the
"License"); you may not use this file except in compliance
with the License.  You may obtain a copy of the License at

  http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing,
software distributed under the License is distributed on an
"AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
KIND, either express or implied.  See the License for the
specific language governing permissions and limitations
under the License.*/

public class TransitionBundle
{
    public WorkerState Source
    {
        get; private set;
    }

    public WorkerState Destination
    {
        get; private set;
    }

    public WorkerFSMOutput Output
    {
        get; private set;
    }

    public TransitionBundle(WorkerState source, WorkerState destination)
    {
        Source = source;
        Destination = destination;
    }

    public TransitionBundle(WorkerState source, WorkerState destination,WorkerFSMOutput output )
    {
        Source = source;
        Destination = destination;
        Output = output;
    }
}