import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'customRuntime'
})
export class CustomRuntimePipe implements PipeTransform {

  transform(value: number, getHoursOnly?: boolean): string {
    let stringValue = value.toString();
    if(stringValue.indexOf(' min') != -1){
      stringValue = stringValue.replace(' min', '');
      value = +stringValue;
    }

    let temp = value * 60;
    let hours = Math.floor((temp / 3600));
    let minutes: number = value % 60;//Math.floor((temp/ 60));
    let minutesS: string = minutes.toString();
    if (minutesS.length == 1) { minutesS = '0' + minutes }
    if (!getHoursOnly) {
      return hours + ':' + minutesS + " (" + stringValue + ' min.) ';
    }
    return hours.toString();
  }
}
