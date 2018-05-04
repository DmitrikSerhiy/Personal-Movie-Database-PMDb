import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'customRuntime'
})
export class CustomRuntimePipe implements PipeTransform {

  transform(value: number): string {
    let  temp = value * 60;
    let hours = Math.floor((temp/3600));
    let minutes: number = value % 60;//Math.floor((temp/ 60));
    let minutesS : string = minutes.toString();
    if(minutesS.length == 1) {minutesS = '0' + minutes}
    return hours + ':' + minutesS;
  }
}
