import { GoogleGenAI } from "@google/genai";
import "dotenv/config"

const ai = new GoogleGenAI({});

export default async function Gemini(prompt) {
  const response = await ai.models.generateContent({
    model: "gemini-2.5-flash",
    contents: prompt,
  });
  return response.text
}
