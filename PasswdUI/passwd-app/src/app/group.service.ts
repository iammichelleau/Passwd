export class GroupService {

  async submit(url: string): Promise<any> {
    const response = await fetch(url, {
      method: 'get'
    });
    console.log(response)

    const text = await response.text() + "; Status: " + response.status + " " + response.statusText;
    
    return text;
  }
}

export default new GroupService();