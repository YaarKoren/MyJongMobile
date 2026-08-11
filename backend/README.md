# Backend Setup

## Activate the virtual environment
Every time you work on the backend, activate the venv first:

cd backend

source venv/Scripts/activate

## Run the server
uvicorn app:app --host 0.0.0.0 --port 8000

## Install dependencies (first time, or after pulling changes)
pip install -r requirements.txt
