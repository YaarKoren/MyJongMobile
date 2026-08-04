from fastapi import FastAPI
from pydantic import BaseModel

app = FastAPI()

class HiMessage(BaseModel):
    message: str

@app.post("/hi")
def confirm_hi(payload: HiMessage):
    print(f"Received from client: {payload.message}")
    return {"message": "backend got your hi!"}