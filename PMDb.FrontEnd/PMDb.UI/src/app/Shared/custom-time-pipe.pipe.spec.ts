import { CustomRuntimePipe } from './custom-time-pipe.pipe';

describe('CustomTimePipePipe', () => {
  it('create an instance', () => {
    const pipe = new CustomRuntimePipe();
    expect(pipe).toBeTruthy();
  });
});
