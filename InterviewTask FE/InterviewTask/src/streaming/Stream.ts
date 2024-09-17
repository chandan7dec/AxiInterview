import { StreamUpdate } from "./StreamUpdate";

export interface Stream<T> {
  start(): void;
  stop(): void;
  setOnUpdate(callback: (updates: Array<StreamUpdate<T>>) => void): void;
}
