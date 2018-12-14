export class UserService {

  async submit(url: string): Promise<any> {
    const response = await fetch(url, {
      method: 'get'
    });

    const text = await response.text() + "; Status: " + response.status + " " + response.statusText;
    
    return text;
  }
}

export default new UserService();